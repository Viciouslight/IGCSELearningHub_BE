using Application.DTOs.Quiz;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IQuizService
    {
        Task<PagedResult<QuizSummaryDTO>> GetListAsync(int? courseId, int page, int pageSize);
        Task<ApiResult<QuizDetailDTO>> GetDetailAsync(int quizId);
        Task<ApiResult<QuizDetailDTO>> CreateAsync(QuizCreateDTO dto);
        Task<ApiResult<QuizDetailDTO>> UpdateAsync(int quizId, QuizUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int quizId);
    }
}
