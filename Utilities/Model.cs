using System;
using System.Collections.Generic;
using global::Login.ST.UEH;

namespace UehInternFrontend
{
    // user ---------------------------------------------------------------
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
        public string? Sdt { get; set; }
        public string? Role { get; set; }
        public string? RoleRestore { get; set; }
        public int? DotInfo { get; set; }
        public string? MaLoai { get; set; }
        public string? MaDot { get; set; }
        public string? MaKhoa { get; set; }
    }

    public class UserLogin
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? Role { get; set; }

        public static explicit operator UserLogin(Student student)
        {
            return new UserLogin
            {
                UserId = student.mssv,
                Name = student.hoten,
                Email = student.email,
                Sdt = student.phone,
                Role = "student"
            };
        }

        public static explicit operator UserLogin(User user)
        {
            return new UserLogin
            {
                UserId = user.Code,
                Name = user.Name,
                Email = user.Email,
                Sdt = user.Sdt,
                Role = "teacher"
            };
        }

    }

    // ---------------------------------------------------------------

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LichsuModel
    {
        public string noidung { get; set; }
        public DateTime ngay { get; set; }
    }

    public class UploadModel
    {
        public Guid Id { get; set; }
        public string? Mssv { get; set; }
        public string? FileType { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
        public DateTime DateTime { get; set; }
        public string? Status { get; set; }
    }


    public class SinhvienModel
    {
        public string? mssv { get; set; }
        public string? ho { get; set; }
        public string? ten { get; set; }
        public string? email { get; set; }
        public string? malop { get; set; }
        public string? ngaysinh { get; set; }
        public string? maloai { get; set; }
        public string? makhoa { get; set; }
        public string? macn { get; set; }
        public string? madot { get; set; }
    }


    public class SinhVienInfo
    {
        public string? mssv { get; set; }
        public string? ho { get; set; }
        public string? ten { get; set; }
        public string? malop { get; set; }
        public string? email { get; set; }
    }

    public class KhoaModel
    {
        public string? makhoa { get; set; }
        public string? tenkhoa { get; set; }
    }

    public class SinhvienkhoaModel
    {
        public string? makhoa { get; set; }
        public string? mssv { get; set; }
    }

    public class GiangvienkhoaModel
    {
        public string? makhoa { get; set; }
        public string? magv { get; set; }
    }

    public class ChuyennganhModel
    {
        public string? macn { get; set; }
        public string? tencn { get; set; }
        public string? makhoa { get; set; }

    }

    public class GiangvienModel
    {
        public string? magv { get; set; }
        public string? tengv { get; set; }
        public string? email { get; set; }
        public string? sdt { get; set; }
        public string? makhoa { get; set; }
        public string? chuyenmon { get; set; }
    }

    public class GiangvienRequest
    {
        public string? magv { get; set; }
        public string? tengv { get; set; }
        public string? email { get; set; }
        public string? sdt { get; set; }
        public string? status { get; set; }
        public string? chuyenmon { get; set; }
    }

    public class GiangvienViewModel
    {
        public string? MaGiangVien { get; set; }
        public string? TenGiangVien { get; set; }
        public int? SoSinhVienHuongDan { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }

    }
    public class ChamcheoViewModel
    {
        public string? magv1 { get; set; }
        public string? magv2 { get; set; }
        public string? tengv1 { get; set; }
        public string? tengv2 { get; set; }
        public string? email1 { get; set; }
        public string? email2 { get; set; }
    }


    public class DangkyModel
    {
        public string mssv { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public string lop { get; set; }
        public string email { get; set; }
        public string magv { get; set; }
        public string makhoa { get; set; }
        public string madot { get; set; }

        public string status { get; set; }
    }

    public class KetquaViewModed
    {
        public string TenSinhVien { get; set; }
        public string MaSinhVien { get; set; }
        public string Lop { get; set; }
        public string Khoa { get; set; }
        public string? Email { get; set; }
        public double Diem { get; set; }
    }

    public class KetquaModel
    {
        public float? tieuchi1 { get; set; }
        public float? tieuchi2 { get; set; }
        public float? tieuchi3 { get; set; }
        public float? tieuchi4 { get; set; }
        public float? tieuchi5 { get; set; }
        public float? tieuchi6 { get; set; }
        public float? tieuchi7 { get; set; }
        public float? diemDN { get; set; }
    }

    public class DotModel
    {
        public string madot { get; set; }
        public string tendot { get; set; }
        public DateTime? ngaybatdau { get; set; }
        public DateTime? ngayketthuc { get; set; }
        public string status { get; set; }

    }

    public class ChitietModel
    {
        public string? emailsv { get; set; }
        public string? tencty { get; set; }
        public string? vitri { get; set; }
        public string? sdtsv { get; set; }
        public string? website { get; set; }
        public string? huongdan { get; set; }
        public string? chucvu { get; set; }
        public string? emailhd { get; set; }
        public string? sdthd { get; set; }
        public string? tendetai { get; set; }
    }


    public class UserModel
    {
        public string email { get; set; }
        public string sdt { get; set; }
    }


    public class UploadResult
    {
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
    }

    public class PhancongRequest
    {
        public string mssv { get; set; }
        public string magv { get; set; }
        public string madot { get; set; }
    }

    public class ChiTietDiem
    {
        public string tenkhoa { get; set; }
        public string tencn { get; set; }
        public string tendot { get; set; }
        public string maloai { get; set; }
        public string hotensv { get; set; }
        public string mssv { get; set; }
        public string lop { get; set; }
        public string tenkl { get; set; }
        public string tencty { get; set; }
        public string tengv { get; set; }
        public string tieuchi1 { get; set; }
        public string tieuchi2 { get; set; }
        public string tieuchi3 { get; set; }
        public string tieuchi4 { get; set; }
        public string tieuchi5 { get; set; }
        public string tieuchi6 { get; set; }
        public string tieuchi7 { get; set; }
        public string diemDN { get; set; }
        public string diemtong { get; set; }
    }

    public class TongHopDiem
    {
        public string tenkhoa { get; set; }
        public string tendot { get; set; }
        public string tenloai { get; set; }
        public string hotensv { get; set; }
        public string mssv { get; set; }
        public string tendetai { get; set; }
        public string tencty { get; set; }
        public string malop { get; set; }
        public string hotengv1 { get; set; }
        public string hotengv2 { get; set; }
        public string diemtong { get; set; }
        public string ngaycham { get; set; }
    }


    public class UserRoleAdminRequest
    {
        public string userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string sdt { get; set; }
        public string role { get; set; }
        public string tenkhoa { get; set; }
    }
}
