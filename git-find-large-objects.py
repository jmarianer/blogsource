import glob
import os.path
import subprocess

def sizeof_fmt(num, suffix='B'):
    """
    Snagged from http://stackoverflow.com/questions/1094841/reusable-library-to-get-human-readable-version-of-file-size
    """
    for unit in ['','Ki','Mi','Gi','Ti','Pi','Ei','Zi']:
        if abs(num) < 1024.0:
            return "%3.1f%s%s" % (num, unit, suffix)
        num /= 1024.0
    return "%.1f%s%s" % (num, 'Yi', suffix)

def lines(command):
    return (subprocess.check_output(command, shell=True)
            .decode("utf-8", "strict")
            .splitlines())

objects_lines = lines('git rev-list --objects --all')
objects = { a[0]: a[1] for a in ((line + ' ').split(' ') for line in objects_lines) }

(git_root, ) = lines('git rev-parse --show-toplevel')
objects_dir = os.path.join(git_root, '.git', 'objects', 'pack')
pack_files = glob.glob(os.path.join(objects_dir, '*.pack'))

if len(pack_files) != 1:
    print 'Please run "git gc".'
    exit()

(pack_file, ) = pack_files

bigobjects_lines = lines('git verify-pack -v ' + pack_file + ' | /usr/bin/sort -rn -k3 | head -20')
bigobjects = ((a[0], a[2]) for a in (line.split() for line in bigobjects_lines))

for md5, size in bigobjects:
    print '%7s' % sizeof_fmt(int(size)), md5, objects[md5]
