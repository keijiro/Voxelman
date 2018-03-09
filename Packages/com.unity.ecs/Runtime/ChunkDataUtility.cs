using Unity.Assertions;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    static unsafe class ChunkDataUtility
    {
        public static int GetIndexInTypeArray(Archetype* archetype, int typeIndex)
        {
            var types = archetype->Types;
            var typeCount = archetype->TypesCount;
            for (var i = 0; i != typeCount; i++)
            {
                if (typeIndex == types[i].TypeIndex)
                    return i;
            }

            return -1;
        }

        public static void GetIndexInTypeArray(Archetype* archetype, int typeIndex, ref int typeLookupCache)
        {
            var types = archetype->Types;
            var typeCount = archetype->TypesCount;

            if (typeLookupCache < typeCount && types[typeLookupCache].TypeIndex == typeIndex)
                return;

            for (var i = 0; i != typeCount; i++)
            {
                if (typeIndex != types[i].TypeIndex)
                    continue;

                typeLookupCache = i;
                return;
            }

            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            throw new System.InvalidOperationException("Shouldn't happen");
            #endif
        }

        public static void GetComponentDataWithTypeAndFixedArrayLength(Chunk* chunk, int index, int typeIndex, out byte* outPtr, out int outArrayLength)
        {
            var archetype = chunk->Archetype;
            var indexInTypeArray = GetIndexInTypeArray(archetype, typeIndex);

            var offset = archetype->Offsets[indexInTypeArray];
            var sizeOf = archetype->SizeOfs[indexInTypeArray];

            outPtr = chunk->Buffer + (offset + sizeOf * index);
            outArrayLength = archetype->Types[indexInTypeArray].FixedArrayLength;
        }

        public static byte* GetComponentDataWithType(Chunk* chunk, int index, int typeIndex, ref int typeLookupCache)
        {
            var archetype = chunk->Archetype;
            GetIndexInTypeArray(archetype, typeIndex, ref typeLookupCache);
            var indexInTypeArray = typeLookupCache;

            var offset = archetype->Offsets[indexInTypeArray];
            var sizeOf = archetype->SizeOfs[indexInTypeArray];

            return chunk->Buffer + (offset + sizeOf * index);
        }

        public static byte* GetComponentDataWithType(Chunk* chunk, int index, int typeIndex)
        {
            var indexInTypeArray = GetIndexInTypeArray(chunk->Archetype, typeIndex);

            var offset = chunk->Archetype->Offsets[indexInTypeArray];
            var sizeOf = chunk->Archetype->SizeOfs[indexInTypeArray];

            return chunk->Buffer + (offset + sizeOf * index);
        }

        public static byte* GetComponentData(Chunk* chunk, int index, int indexInTypeArray)
        {
            var offset = chunk->Archetype->Offsets[indexInTypeArray];
            var sizeOf = chunk->Archetype->SizeOfs[indexInTypeArray];

            return chunk->Buffer + (offset + sizeOf * index);
        }

        public static void Copy(Chunk* srcChunk, int srcIndex, Chunk* dstChunk, int dstIndex, int count)
        {
            Assert.IsTrue(srcChunk->Archetype == dstChunk->Archetype);

            var arch = srcChunk->Archetype;
            var srcBuffer = srcChunk->Buffer;
            var dstBuffer = dstChunk->Buffer;
            var offsets = arch->Offsets;
            var sizeOfs = arch->SizeOfs;
            var typesCount = arch->TypesCount;

            for (var t = 0; t < typesCount; t++)
            {
                var offset = offsets[t];
                var sizeOf = sizeOfs[t];
                var src = srcBuffer + (offset + sizeOf * srcIndex);
                var dst = dstBuffer + (offset + sizeOf * dstIndex);

                UnsafeUtility.MemCpy(dst, src, sizeOf * count);
            }
        }

        public static void ClearComponents(Chunk* dstChunk, int dstIndex, int count)
        {
            var arch = dstChunk->Archetype;

            var offsets = arch->Offsets;
            var sizeOfs = arch->SizeOfs;
            var dstBuffer = dstChunk->Buffer;
            var typesCount = arch->TypesCount;

            for (var t = 1; t != typesCount; t++)
            {
                var offset = offsets[t];
                var sizeOf = sizeOfs[t];
                var dst = dstBuffer + (offset + sizeOf * dstIndex);

                UnsafeUtility.MemClear(dst, sizeOf * count);
            }
        }

        public static void ReplicateComponents(Chunk* srcChunk, int srcIndex, Chunk* dstChunk, int dstBaseIndex, int count)
        {
            Assert.IsTrue(srcChunk->Archetype == dstChunk->Archetype);

            var arch = srcChunk->Archetype;
            var srcBuffer = srcChunk->Buffer;
            var dstBuffer = dstChunk->Buffer;
            var offsets = arch->Offsets;
            var sizeOfs = arch->SizeOfs;
            var typesCount = arch->TypesCount;
            // type[0] is always Entity, and will be patched up later, so just skip
            for (var t = 1; t != typesCount; t++)
            {
                var offset = offsets[t];
                var sizeOf = sizeOfs[t];
                var src = srcBuffer + (offset + sizeOf * srcIndex);
                var dst = dstBuffer + (offset + sizeOf * dstBaseIndex);

                UnsafeUtility.MemCpyReplicate(dst, src, sizeOf, count);
            }
        }

        public static void Convert(Chunk* srcChunk, int srcIndex, Chunk* dstChunk, int dstIndex)
        {
            var srcArch = srcChunk->Archetype;
            var dstArch = dstChunk->Archetype;

            var srcI = 0;
            var dstI = 0;
            while (srcI < srcArch->TypesCount && dstI < dstArch->TypesCount)
            {
                if (srcArch->Types[srcI] < dstArch->Types[dstI])
                    ++srcI;
                else if (srcArch->Types[srcI] > dstArch->Types[dstI])
                    ++dstI;
                else
                {
                    var src = srcChunk->Buffer + srcArch->Offsets[srcI] + srcIndex * srcArch->SizeOfs[srcI];
                    var dst = dstChunk->Buffer + dstArch->Offsets[dstI] + dstIndex * dstArch->SizeOfs[dstI];
                    UnsafeUtility.MemCpy(dst, src, srcArch->SizeOfs[srcI]);
                    ++srcI;
                    ++dstI;
                }
            }
        }

        public static void CopyManagedObjects(ArchetypeManager typeMan, Chunk* srcChunk, int srcStartIndex, Chunk* dstChunk, int dstStartIndex, int count)
        {
            var srcArch = srcChunk->Archetype;
            var dstArch = dstChunk->Archetype;

            var srcI = 0;
            var dstI = 0;
            while (srcI < srcArch->TypesCount && dstI < dstArch->TypesCount)
            {
                if (srcArch->Types[srcI] < dstArch->Types[dstI])
                    ++srcI;
                else if (srcArch->Types[srcI] > dstArch->Types[dstI])
                    ++dstI;
                else
                {
                    if (srcArch->ManagedArrayOffset[srcI] >= 0)
                    {
                        for (var i = 0; i < count; ++i)
                        {
                            var obj = typeMan.GetManagedObject(srcChunk, srcI, srcStartIndex+i);
                            typeMan.SetManagedObject(dstChunk, dstI, dstStartIndex+i, obj);
                        }
                    }
                    ++srcI;
                    ++dstI;
                }
            }
        }
        public static void ClearManagedObjects(ArchetypeManager typeMan, Chunk* chunk, int index, int count)
        {
            var arch = chunk->Archetype;

            for (var type = 0; type < arch->TypesCount; ++type)
            {
                if (arch->ManagedArrayOffset[type] < 0)
                    continue;

                for (var i = 0; i < count; ++i)
                    typeMan.SetManagedObject(chunk, type, index+i, null);
            }
        }
    }
}
