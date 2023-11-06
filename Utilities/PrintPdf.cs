using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace UehInternFrontend
{
    public class PrintPDF
    {

        private static string PageBreak ()
        {
            string htmlString = $@"
                <div class='pagebreak'></div>
            ";

            return htmlString;
        }

        private static string ChiTietDiemHTML (string type, ChiTietDiem? diem = null)
        {
            string htmlString = "";

            if (type == "style")
            {
                // style
                htmlString = @"
                    <style>
                        body {
                            font-size: 16px;
                            font-family: 'Times New Roman', Times, serif;
                            line-height: 0.8;
                        }

                        @page {
                            size: A4 portrait;
                            margin: 1cm;
                            marks: crop;
                            bleed: 3mm;
                            orphans: 2;
                            widows: 2;
                        }

                        @media print {
                            body {
                                -webkit-print-color-adjust: exact;
                            }

                            .pagebreak {
                                page-break-before: always;
                            } /* page-break-after works, as well */
                        }

                        .center {
                            text-align: center;
                        }

                        .right {
                            text-align: right;
                        }

                        .no-wrap {
                            white-space: nowrap;
                        }

                        .itatic {
                            font-weight: 100;
                            font-style: italic;
                        }

                        /* pretitle */

                        .pretitle {
                            display: flex;
                            justify-content: space-between;
                            align-items: center;
                        }

                        .pretitle p {
                            line-height: 0.8;
                        }

                        /* title */

                        .title {
                            text-align: center;
                        }

                        .title .lable {
                            font-size: 22px;
                        }

                        /* infosv */

                        .infosv {
                            display: flex;
                            flex-wrap: wrap;
                            margin-top: 20px;
                        }

                        .infosv > * {
                            flex-shrink: 0;
                            width: 100%;
                            max-width: 100%;
                            margin: 4px 0;
                            padding: 0;
                        }

                        .infosv .col-6 {
                            flex: 0 0 auto;
                            width: 50%;
                        }

                        .infosv .col-12 {
                            flex: 0 0 auto;
                            width: 100%;
                        }

                        .infosv .col-6 {
                            flex: 0 0 auto;
                            width: 50%;
                        }

                        .infogv {
                            display: flex;
                            margin-top: 22px;
                            width: 100%;
                        }

                        .infogv > * {
                            margin-top: 20px;
                            margin-bottom: 0;
                        }
                        .infogv .name {
                            flex-grow: 1;
                        }

                        .infogv .isgv {
                            flex-grow: 0;
                        }

                        .infogv .checkbox {
                            width: 16px;
                            height: 16px;
                        }

                        /* table score */

                        .score-title {
                            font-weight: 700;
                            text-decoration: underline;
                            margin: 12px 0;
                        }

                        .score-wrapper {
                            display: flex;
                            justify-content: center;
                        }

                        table {
                            width: 95%;
                        }

                        table,
                        th,
                        td {
                            border: 1px solid #393939;
                            border-collapse: collapse;
                        }

                        th,
                        td {
                            padding: 8px;
                        }

                        td p {
                            margin: 4px 0;
                        }

                        td p + p {
                            margin: 6px 0;
                        }

                        .table-title {
                            background-color: rgb(191, 191, 191);
                        }

                        .footer {
                            margin: 20px 0;
                            display: flex;
                            flex-direction: row-reverse;
                        }

                        .sign {
                            width: 50%;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                        }

                        .sign > * {
                            margin: 6px 0;
                        }

                        .sign-name {
                            margin-top: 60px;
                        }
                    </style>
                ";
            }

            if (type == "content")
            {
                // body
                htmlString += $@"
                    <div>
                        <div class='pretitle'>
                            <div>
                                <p><strong>Đại học UEH – Trường Công nghệ và Thiết kế</strong></p>
                                <p><strong>Khoa {diem.tenkhoa}</strong></p>
                                <p><strong>Chuyên ngành {diem.tencn}</strong></p>
                            </div>
                            <div>
                                <p class='center'><strong>Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam</strong></p>
                                <p class='center'><strong>Độc lập – Tự do – Hạnh phúc</strong></p>
                            </div>
                        </div>

                        <div class='title'>
                            <p class='lable'><strong>BẢNG ĐIỂM CHI TIẾT </strong></p>
                            <p class='lable'><strong>THỰC TẬP TỐT NGHIỆP CHO SINH VIÊN </strong></p>
                            <p class='lable'><strong>ĐỢT {diem.tendot.ToUpper()}</strong></p>
                            <p class='itatic'><em>(Dành cho tất cả hình thức thực tập tốt nghiêp)</em></p>
                        </div>

                        <div class='infosv'>
                            <p class='col-6'>Họ tên sinh viên: {diem.hotensv}</p>
                            <p class='col-6'>Mã số sinh viên: {diem.mssv}</p>
                            <p class='col-6'>Khoá:</p>
                            <p class='col-6'>Lớp: {diem.lop}</p>

                            {(diem.maloai == "HKDN" ? $@"
                                <p class='col-6'>Tên công ty: {diem.tencty}</p>
                            " : $@"
                                <p class='col-6'>Tên khoá luận: {diem.tenkl}</p>
                            ")}

                        </div>

                        <div class='infogv'>
                            <p class='name'><strong>Họ tên giáo viên chấm:</strong> {diem.tengv}</p>
                            <p class='isgv'>
                                <strong>Là người hướng dẫn: </strong>

                                <svg class='checkbox' viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg' fill='#000000'>
                                    <g id='SVGRepo_bgCarrier' stroke-width='0.5'></g>
                                    <g id='SVGRepo_tracerCarrier' stroke-linecap='round' stroke-linejoin='round'></g>
                                    <g id='SVGRepo_iconCarrier'>
                                        <path fill='none' stroke='#000000' stroke-width='2'
                                            d='M2,2 L22,2 L22,22 L2,22 L2,2 Z M5,13 L10,17 L19,6'>
                                        </path>
                                    </g>
                                </svg>

                            </p>
                        </div>

                        <div class='score-title'>Điểm thành phần</div>
                        <div class='score-wrapper'>
                            <table>
                                <tr class='table-title'>
                                    <td><strong>STT</strong></td>
                                    <td><strong>Tiêu chí</strong></td>
                                    <td class='no-wrap right'><strong>Điểm /điểm tối đa</strong></td>

                                </tr>
                                <tr>
                                    <td class='center'>1</td>
                                    <td>Về vấn đề được đặt ra hay mục tiêu (dựa trên có hay không, tính rõ ràng, tính hợp thời, tính mức độ
                                        cấp thiết, tính mức độ phức tạp,…)</td>
                                    <td class='right'>{diem.tieuchi1} / 1</td>

                                </tr>
                                <tr>
                                    <td class='center'>2</td>
                                    <td>
                                        <p>Phương pháp giải quyết vấn đề hay phương pháp để đạt mục tiêu:</p>
                                        <p>+ Rõ ràng và hợp lý | đúng.</p>
                                        <p>+ Mức độ áp dụng kiến thức ngành đã học | tự học.</p>
                                        <p>+ Hợp thời đại | thiết thực.</p>
                                    </td>
                                    <td class='right'>{diem.tieuchi2} / 1.5</td>

                                </tr>
                                <tr>
                                    <td class='center'>3</td>
                                    <td>
                                        <p>Kỹ năng giải quyết vấn đề và kết quả đạt được so với mục tiêu, gồm:</p>
                                        <p>+ Kỹ năng phân tích nghiệp vụ</p>
                                        <p>+ Kỹ năng phân tích mô hình | hệ thống | giải pháp.</p>
                                        <p>+ Kỹ năng thiết kế mô hình | hệ thống | giải pháp.</p>
                                        <p>+ Kỹ năng thiết kế dữ liệu.p>
                                        <p>+ Kỹ năng thu thập và phân tích dữ liệu.</p>
                                        <p>+ Kỹ năng lập trình.</p>
                                        <p>+ Kỹ năng sử dụng, vận dụng các công cụ công nghệ giải quyết các vấn đề.</p>
                                        <p>+ Kỹ năng lập kế hoạch và chiến lược công nghệ.</p>
                                        <p>+ Kỹ năng sử dụng, vận dụng các công cụ công nghệ giải quyết các vấn đề.</p>
                                        <p>+ …</p>
                                        <p class='itatic'>(Ít nhất phải thể hiện được 3 kỹ năng, mỗi kỹ năng tối đa được 3 điểm)</p>
                                    </td>
                                    <td class='right'>{diem.tieuchi3} / 5</td>
                                </tr>
                                <tr>
                                    <td class='center'>4</td>
                                    <td>
                                        <p>Mức độ kết quả đạt được so với mục tiêu đã đề ra</p>
                                    </td>
                                    <td class='right'>{diem.tieuchi4} / 1</td>

                                </tr>
                                <tr>
                                    <td class='center'>5</td>
                                    <td>
                                        <p>Cách thức trình bày nội dung</p>
                                    </td>
                                    <td class='right'>{diem.tieuchi5} / 1</td>

                                </tr>
                                <tr>
                                    <td class='center'>6</td>
                                    <td>
                                        <p>Tuân thủ quy định làm thực tập tốt nghiệp (dựa trên thái độ, hành vi, tính chuyên cần, …)</p>
                                    </td>
                                    <td class='right'>{diem.tieuchi6} / 0.5</td>

                                </tr>
                                <tr>
                                    <td class='center'>7</td>
                                    <td>
                                        <p>Điểm cộng thêm cho một số trường hợp đặc biệt:<p>
                                        <p>+ Bài mang tính mới, giải quyết được và cho kết quả chấp nhận được.</p>
                                        <p>+ Có bài báo được đăng trên các tạp chí khoa học.</p>
                                    </td>

                                    <td class='right'>{diem.tieuchi7} / 1</td>
                                </tr>

                                {(diem.maloai == "HKDN" ? $@"
                                <tr>
                                    <td class='center'>8</td>
                                    <td>
                                        <p>Điểm doanh nghiệp chấm<p>
                                    </td>

                                    <td class='right'>{diem.diemDN} / 10</td>
                                </tr>
                                " : "")}

                                <tr class='lable '>
                                    <td></td>
                                    <td><strong>Điểm tổng cộng</strong></td>
                                    <td class='right'><strong>{diem.diemtong}/10</strong></td>

                                </tr>
                            </table>
                        </div>

                        <div class='footer'>
                            <div class='sign'>
                                <p><strong>Giáo viên chấm</strong></p>
                                <p class='itatic'>(Ký tên và ghi rõ họ tên)</p>
                                <p class='sign-name'>{diem.tengv}</p>
                            </div>
                        </div>

                    </div>
                    ";

            }

            return htmlString;
        }

        private static string TongHopDiemHTML (string type, List<TongHopDiem>? bangdiem = null)
        {
            string htmlString = "";

            if (type == "style")
            {
            // style
            htmlString = @"
                <style>
                    body {
                        font-size: 16px;
                        font-family: 'Times New Roman', Times, serif;
                        line-height: 0.8;
                    }

                    @media print {
                        body {
                            -webkit-print-color-adjust: exact;
                        }

                        .pagebreak {
                            page-break-before: always;
                        } /* page-break-after works, as well */
                    }

                    .center {
                        text-align: center;
                    }

                    .right {
                        text-align: right;
                    }

                    .no-wrap {
                        white-space: nowrap;
                    }

                    .itatic {
                        font-weight: 100;
                        font-style: italic;
                    }

                    /* pretitle */

                    .pretitle {
                        display: flex;
                        justify-content: space-between;
                        align-items: center;
                    }

                    .pretitle p {
                        line-height: 0.8;
                    }

                    /* title */

                    .title {
                        text-align: center;
                    }

                    .title .lable {
                        font-size: 22px;
                    }

                    .table-wrapper {
                        display: flex;
                        justify-content: center;
                        padding: 12px 0;
                    }

                    table {
                        width: 98%;
                    }

                    table,
                    th,
                    td {
                        border: 1px solid #393939;
                        border-collapse: collapse;
                    }

                    th,
                    td {
                        padding: 8px;
                    }

                    td p {
                        margin: 4px 0;
                    }

                    td p + p {
                        margin: 6px 0;
                    }

                    .table-title {
                        background-color: rgb(191, 191, 191);
                    }
                </style>
            ";
            }

            if (type == "content")
            {
                // body
                htmlString += $@"
                    <div>
                        <div class='pretitle'>
                            <div>
                                <p><strong>Đại học UEH – Trường Công nghệ và Thiết kế</strong></p>
                                <p><strong>Khoa {bangdiem[0].tenkhoa}</strong></p>
                                <p><strong>Chuyên ngành</strong></p>
                            </div>
                            <div>
                                <p class='center'><strong>Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam</strong></p>
                                <p class='center'><strong>Độc lập – Tự do – Hạnh phúc</strong></p>
                            </div>
                        </div>

                        <div class='title'>
                            <p class='lable'><strong>BẢNG ĐIỂM TỔNG HỢP - THỰC TẬP TỐT NGHIỆP</strong></p>
                            <p class='lable'><strong>ĐỢT {bangdiem[0].tendot.ToUpper()}</strong></p>
                            <p class='lable'><strong>HÌNH THỨC {bangdiem[0].tenloai.ToUpper()}</strong></p>
                            <p class='itatic'><em>(Lưu ý: nếu loại hình thức 'học kỳ doanh nghiệp' thì không có giáo viên chấm 2)</em></p>
                        </div>

                        <div class='table-wrapper'>
                            <table>
                                <tr class='table-title'>
                                    <td class='center'><strong>STT</strong></td>
                                    <td><strong>Mã số sinh viên | Lớp | Khóa</strong></td>
                                    <td><strong>Tên sinh viên</strong></td>
                                    <td><strong>{(bangdiem[0].tenloai.ToUpper().Contains("DOANH NGHIỆP")?"Tên công ty":"Tên đề tài")}</strong></td>
                                    <td><strong>Điểm cuối cùng</strong></td>
                                </tr>
                ";

                for (int i = 0; i < bangdiem.Count; i++)
                {
                    htmlString += $@"
                        <tr>
                            <td class='center'>{i + 1}</td>
                            <td>{bangdiem[i].mssv} | {bangdiem[i].malop}</td>
                            <td>{bangdiem[i].hotensv}</td>
                            <td>{(bangdiem[0].tenloai.ToUpper().Contains("DOANH NGHIỆP")?bangdiem[i].tencty:bangdiem[i].tendetai)}</td>
                            <td>{bangdiem[i].diemtong}</td>
                        </tr>
                    ";
                }

                htmlString += $@"
                            </table>

                        </div>
                        <div>
                            <p><strong>Ngày chấm:</strong> {bangdiem[0].ngaycham}</p>
                            <p><strong>Giáo viên hướng dẫn và chấm 1:</strong> {bangdiem[0].hotengv1}</p>
                            <p><strong>Giáo viên chấm 2:</strong> {bangdiem[0].hotengv2}</p>
                        </div>

                    </div>
                ";
            }

            return htmlString;
        }

        public static async Task<string> ChiTietDiem(IJSRuntime js, ChiTietDiem diem)
        {
            string htmlString = "";

            htmlString += PrintPDF.ChiTietDiemHTML("style");
            htmlString += PrintPDF.ChiTietDiemHTML("content", diem);
            htmlString += PrintPDF.PageBreak();

            await js.InvokeVoidAsync("PrintPDF", htmlString, "BangDiemChiTiet_" + diem.mssv);
            return null;
        }

        public static async Task<string> AllChiTietDiem(IJSRuntime js, List<ChiTietDiem> dsdiem)
        {
            string htmlString = "";


            for (int i = 0; i < dsdiem.Count; i++)
            {
                if (i == 0)
                {
                    htmlString += PrintPDF.ChiTietDiemHTML("style");
                    htmlString += PrintPDF.ChiTietDiemHTML("content", dsdiem[i]);
                }
                else
                {
                    htmlString += PrintPDF.PageBreak();
                    htmlString += PrintPDF.ChiTietDiemHTML("content", dsdiem[i]);
                }
            }

            await js.InvokeVoidAsync("PrintPDF", htmlString, "ToanBoBangDiemChiTiet");
            return null;
        }

        public static async Task<string> TongHopDiem(IJSRuntime js, List<TongHopDiem> bangdiemKLTN, List<TongHopDiem> bangdiemHKDN)
        {
            string htmlString = "";

            htmlString += PrintPDF.TongHopDiemHTML("style");
            htmlString += PrintPDF.TongHopDiemHTML("content", bangdiemKLTN);
            htmlString += PrintPDF.PageBreak();
            htmlString += PrintPDF.TongHopDiemHTML("content", bangdiemHKDN);

            await js.InvokeVoidAsync("PrintPDF", htmlString, "PhieuDiemTongHop");
            return null;
        }
    }
}
