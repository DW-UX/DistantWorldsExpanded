using System;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;

internal static class Helpers {

  public static unsafe void WithMappedFileContentStream(this FileInfo file, Action<UnmanagedMemoryStream> action) {
    if (file is null) throw new ArgumentNullException(nameof(file));
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (!file.Exists) throw new FileNotFoundException(file.FullName);

    var path = file.FullName;
    var size = file.Length;
    using var mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, size, MemoryMappedFileAccess.Read);
    using var accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.Read);
    byte* pointer = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
    try {
      using var ums = new UnmanagedMemoryStream(pointer, size);
      action(ums);
    }
    finally {
      accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
  }

  public static unsafe void WithMappedFileContentStreamSerial(this FileInfo file, params Action<UnmanagedMemoryStream>[] action) {
    if (file is null) throw new ArgumentNullException(nameof(file));
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (!file.Exists) throw new FileNotFoundException(file.FullName);

    var path = file.FullName;
    var size = file.Length;
    using var mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, size, MemoryMappedFileAccess.Read);
    using var accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.Read);
    byte* pointer = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
    try {
      foreach ( var act in action ) {
        using var ums = new UnmanagedMemoryStream(pointer, size);
        act(ums);
      }
    }
    finally {
      accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
  }

  public static unsafe Task[] WithMappedFileContentStreamParallel(this FileInfo file, params Action<UnmanagedMemoryStream>[] action) {
    if (file is null) throw new ArgumentNullException(nameof(file));
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (!file.Exists) throw new FileNotFoundException(file.FullName);

    var path = file.FullName;
    var size = file.Length;
    using var mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, size, MemoryMappedFileAccess.Read);
    using var accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.Read);
    byte* pointer = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
    try {
      var tasks = new Task[action.Length];
      
      for (var i = 0; i < action.Length; i++) {
        var act = action[i];
        var ums = new UnmanagedMemoryStream(pointer, size);
        tasks[i] = Task.Run(() => {
          try {
            act(ums);
          }
          finally {
            ums.Dispose();
          }
        });
      }

      return tasks;
    }
    finally {
      accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
  }

}
