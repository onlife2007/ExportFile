using System;
using System.Globalization;
using CsvHelper;

namespace ExportFile
{
    public static class ExportFunc
    {
        public static void ExportFiles()
        {
            int totalGroup = 10000;
            int totalStaff = 100000;

            // Export group.csv
            int? parent = 0;
            var groups = new List<Group>();
            for (int i = 0; i < totalGroup; i++)
            {
                var group = new Group()
                {
                    GroupId = i + 1,
                    GroupName = $"Group {i + 1}",
                    ParentGroupId = i == 0 ? null : parent
                };
                if (i % 20 == 0)
                {
                    parent++;
                }
                groups.Add(group);

            }

            using (var writer = new StreamWriter("groups.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(groups);
            }

            // Export staff.csv
            var staffs = new List<Staff>();
            for (int i = 0; i < totalStaff; i++)
            {
                var staff = new Staff()
                {
                    StaffId = i + 1,
                    StaffName = $"Staff {i + 1}",
                    StaffType = new Random().Next(1, 8)
                };
                staffs.Add(staff);

            }

            using (var writer = new StreamWriter("staff.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(staffs);
            }

            // Export staffgroup.csv
            var staffGroups = new List<StaffGroup>();
            for (int i = 0; i < totalStaff * 1.2; i++)
            {
                var staffGroup = new StaffGroup()
                {
                    StaffId = new Random().Next(1, totalStaff + 1),
                    GroupId = new Random().Next(1, totalGroup + 1)
                };
                staffGroups.Add(staffGroup);

            }

            using (var writer = new StreamWriter("staffgroup.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(staffGroups);
            }
        }
    }
}

