using Beyova;
using Beyova.ExceptionSystem;
using EF.E1Technology.EEO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Beyova.HttpConstants;

namespace EF.E1Technology.EEO
{
    /// <summary>
    /// implement all api in this file
    /// </summary>
    public partial class  EEORestApiClient
    {
        #region Public methods

        /// <summary>
        /// Gets the user course list.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <param name="beginTime">The begin time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public List<CourseInfo> GetUserCourseList(string userAccount, DateTime? beginTime = null, DateTime? endTime = null)
        {
            try
            {
                userAccount.CheckEmptyString(nameof(userAccount));

                var data = new Dictionary<string, string>();

                data.Add("userAccount", userAccount);
                data.AddIfNotNullOrEmpty("beginTime", (beginTime.ToUnixMillisecondsDateTime() / 1000).SafeToString());
                data.AddIfNotNullOrEmpty("endTime", (endTime.ToUnixMillisecondsDateTime() / 1000).SafeToString());

                return Invoke<List<CourseInfo>>(ModuleNames.Course, "getUserCourseList", HttpMethod.Post, data);
            }
            catch (Exception ex)
            {
                throw ex.Handle(new { userAccount, beginTime, endTime });
            }
        }

        /// <summary>
        /// get student list
        /// </summary>
        /// <param name="page">current page, default equal to 1</param>
        /// <param name="perpage">current perpage,default equal to 20</param>
        /// <returns></returns>
        public List<StudentInfo> GetStudentList(int? page = 1, int? perpage = 20)
        {
            try
            {
                var data = new Dictionary<string, string>();
                data.AddIfNotNullOrEmpty("page", page.SafeToString());
                data.AddIfNotNullOrEmpty("perpage", perpage.SafeToString());

                return Invoke<List<StudentInfo>>(ModuleNames.Course, "getStudentList", HttpMethod.Post, data);
            }
            catch (Exception ex)
            {
                throw ex.Handle(new { page, perpage });
            }
        }

        /// <summary>
        /// register student which will register user at the same time,
        /// return the student UID
        /// </summary>
        /// <param name="telephone">telephone</param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="md5pass"></param>
        /// <param name="FileData"></param>
        /// <returns></returns>
        public Int64 Register(string telephone,string nickname,string password,string md5pass,string FileData)
        {
            try
            {
                telephone.CheckEmptyString(nameof(telephone));
                //
                if (string.IsNullOrEmpty(password))
                {
                    md5pass.CheckEmptyString(nameof(md5pass));
                }

                if (string.IsNullOrEmpty(md5pass))
                {
                    password.CheckEmptyString(nameof(password));
                }

                var data = new Dictionary<string, string>();
                data.Add("telephone", telephone);
                data.AddIfNotNullOrEmpty("nickname", nickname);
                data.AddIfNotNullOrEmpty("password", password);
                data.AddIfNotNullOrEmpty("md5pass", md5pass);
                data.AddIfNotNullOrEmpty("FileData", FileData);

                return Invoke<Int64>(ModuleNames.Course, "register", HttpMethod.Post, data);
            }

            catch(Exception ex)
            {
                throw ex.Handle(new { telephone, nickname, password, md5pass, FileData });
            }
        }

        /// <summary>
        /// add teacher,which will return teacher UID
        /// </summary>
        /// <param name="teacherAccount"></param>
        /// <param name="teacherName"></param>
        /// <param name="FileData"></param>
        /// <returns></returns>
        public Int64 AddTeacher(string teacherAccount,string teacherName,string FileData)
        {
            try
            {
                teacherAccount.CheckEmptyString(nameof(teacherAccount));
                teacherName.CheckEmptyString(nameof(teacherName));

                var data = new Dictionary<string, string>();
                data.Add("teacherAccount", teacherAccount);
                data.Add("teacherName", teacherName);
                data.AddIfNotNullOrEmpty("FileData", FileData);

                return Invoke<Int64>(ModuleNames.Course, "addTeacher", HttpMethod.Post, data);
            }
            catch(Exception ex)
            {
                throw ex.Handle(new { teacherAccount, teacherName, FileData });
            }
        }

        /// <summary>
        /// add one course
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="folderId"></param>
        /// <param name="FileData"></param>
        /// <param name="expiryTime"></param>
        /// <param name="mainTeacherAccount"></param>
        /// <param name="courseIntroduce"></param>
        /// <returns></returns>
        public Int64 AddCourse(string courseName,Int64? folderId,string FileData,DateTime? expiryTime,string mainTeacherAccount,string courseIntroduce)
        {
            try
            {
                courseName.CheckEmptyString(nameof(courseName));
                // transfer datetime to timestamp
                var timeStamp = (expiryTime ?? DateTime.UtcNow.AddDays(90)).ToUnixMillisecondsDateTime() / 1000;
                var data = new Dictionary<string, string>();
                data.Add("courseName", courseName);
                data.AddIfNotNullOrEmpty("folderId", folderId.Value.ToString());
                data.AddIfNotNullOrEmpty("FileData", FileData);
                data.AddIfNotNullOrEmpty("expiryTime", timeStamp.ToString());
                data.AddIfNotNullOrEmpty("mainTeacherAccount", mainTeacherAccount);
                data.AddIfNotNullOrEmpty("courseIntroduce", courseIntroduce);

                return Invoke<Int64>(ModuleNames.Course, "addCourse", HttpMethod.Post, data);
            }

            catch(Exception ex)
            {
                throw ex.Handle(new { });
            }
        }

        /// <summary>
        /// add one class for course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Int64 AddCourseClass(Course course)
        {
            try
            {
                course.CourseId.CheckNullObject(nameof(course.CourseId));
                course.CourseDetail.ClassName.CheckEmptyString(nameof(course.CourseDetail.ClassName));
                course.CourseDetail.BeginTime.CheckNullObject(nameof(course.CourseDetail.BeginTime));
                course.CourseDetail.EndTime.CheckNullObject(nameof(course.CourseDetail.EndTime));
                course.CourseDetail.TeacherAccount.CheckEmptyString(nameof(course.CourseDetail.TeacherAccount));
                course.CourseDetail.TeacherName.CheckEmptyString(nameof(course.CourseDetail.TeacherName));

                //
                var data = new Dictionary<string, string>();
                data.Add("courseId", course.CourseId.ToString());
                data.Add("className", course.CourseDetail.ClassName);
                data.Add("beginTime", course.CourseDetail.BeginTime.ToString());
                data.Add("endTime", course.CourseDetail.EndTime.ToString());
                data.Add("teacherAccount", course.CourseDetail.TeacherAccount);
                data.Add("teacherName", course.CourseDetail.TeacherName);

                data.AddIfNotNullOrEmpty("folderId", course.CourseDetail.FolderId.Value.ToString());
                data.AddIfNotNullOrEmpty("seatNum", course.CourseDetail.SeatNum.ToString());
                data.AddIfNotNullOrEmpty("record", Convert.ToInt16(course.CourseDetail.Record).ToString());
                data.AddIfNotNullOrEmpty("live", Convert.ToInt16(course.CourseDetail.Live).ToString());
                data.AddIfNotNullOrEmpty("replay", Convert.ToInt16(course.CourseDetail.Replay).ToString());
                data.AddIfNotNullOrEmpty("assistantAccount", course.CourseDetail.AssistantAccount);

                return Invoke<Int64>(ModuleNames.Course, "addCourseClass", HttpMethod.Post, data);
            }

            catch (Exception ex)
            {
                throw ex.Handle(course);
            }
        }

        /// <summary>
        /// add multiple classes for course
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="detailList"></param>
        /// <returns></returns>
        public List<CourseResult> AddCourseClassMultiple(Int64? courseId,List<CourseDetail> detailList)
        {
            try
            {
                courseId.Value.CheckNullObject(nameof(courseId));
                //check each
                foreach(CourseDetail detail in detailList)
                {
                    detail.ClassName.CheckEmptyString(nameof(detail.ClassName));
                    detail.BeginTime.CheckNullObject(nameof(detail.BeginTime));
                    detail.EndTime.CheckNullObject(nameof(detail.EndTime));
                    detail.TeacherAccount.CheckEmptyString(nameof(detail.TeacherAccount));
                    detail.TeacherName.CheckEmptyString(nameof(detail.TeacherName));
                }

                var data = new Dictionary<string, string>();
                data.Add("courseId", courseId.Value.ToJson());
                data.AddIfNotNullOrEmpty("classJson", detailList.ToJson());

                return Invoke<List<CourseResult>>(ModuleNames.Course, "addCourseClassMultiple", HttpMethod.Post, data);
            }

            catch (Exception ex)
            {
                throw ex.Handle(new { courseId = courseId, detailList = detailList });
            }
        }

        /// <summary>
        /// add multiple students for course
        /// </summary>
        /// <param name="courseStudent"></param>
        /// <returns></returns>
        public List<BaseResult> AddClassStudentMultiple(CourseStudent courseStudent)
        {
            try
            {
                courseStudent.CourseId.CheckNullObject(nameof(courseStudent.CourseId));
                courseStudent.Identity.CheckNullObject(nameof(courseStudent.Identity));
                courseStudent.IsRegister.CheckNullObject(nameof(courseStudent.IsRegister));

                foreach(Student stdt in courseStudent.StudentList)
                {
                    stdt.Account.CheckEmptyString(nameof(stdt.Account));
                    stdt.Name.CheckEmptyString(nameof(stdt.Name));
                }

                var data = new Dictionary<string, string>();
                data.Add("courseId", courseStudent.CourseId.ToString());
                data.Add("identity", courseStudent.Identity.ToString());
                data.Add("isRegister", Convert.ToInt16(courseStudent.IsRegister).ToString());
                data.Add("studentJson", courseStudent.ToJson());

                return Invoke<List<BaseResult>>(ModuleNames.Course, "addClassStudentMultiple", HttpMethod.Post, data);

            }

            catch (Exception ex)
            {
                throw ex.Handle(new { courseStudent });
            }
        }
        #endregion
    }
}
