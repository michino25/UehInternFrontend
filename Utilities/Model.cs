using System;
using System.Collections.Generic;
using global::Login.ST.UEH;

namespace UehInternFrontend
{
    public class Student
    {
        public string? mssv { get; set; }
        public string? email { get; set; }
        public string? hoten { get; set; }
        public string? lop { get; set; }
        public string? khoa { get; set; }
        public string? cmnd { get; set; }
        public string? classstudentname { get; set; }
        public string? birthday { get; set; }
        public string? birthplace { get; set; }
        public string? studystatusid { get; set; }
        public string? phone { get; set; }
        public string? HDT { get; set; }

        public static explicit operator Student(Login.ST.UEH.RSStudent student)
        {
            return new Student
            {
                mssv = student.mssv,
                email = student.email,
                hoten = student.hoten,
                lop = student.lop,
                khoa = student.khoa,
                cmnd = student.cmnd,
                classstudentname = student.classstudentname,
                birthday = student.birthday,
                birthplace = student.birthplace,
                studystatusid = student.studystatusid,
                phone = student.phone,
                HDT = student.HDT
            };
        }

    }

    public class User
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public static explicit operator User(Student student)
        {
            return new User
            {
                Code = student.mssv,
                Email = student.email,
                Name = student.hoten,
                Role = "sinhvien"
            };
        }

    }

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class UploadModel
    {

        public Guid Id { get; set; }
        public string Mssv { get; set; }
        public string FileType { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }


    public class SinhvienModel
    {
        public string mssv { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public string thuoclop { get; set; }
        public string khoagoc { get; set; }
        public string khoahoc { get; set; }
        public string mahp { get; set; }
        public string malhp { get; set; }
        public string tenhp { get; set; }
        public string soct { get; set; }
        public string malop { get; set; }
        public string bacdt { get; set; }
        public string loaihinh { get; set; }
        public string? macn { get; set; }


    }
    public class KhoaModel
    {
        public string makhoa { get; set; }
        public string tenkhoa { get; set; }

    }

}
