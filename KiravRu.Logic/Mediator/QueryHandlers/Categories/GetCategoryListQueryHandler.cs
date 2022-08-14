using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Mediator.Queries.Categories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Categories
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, GetCategoryListQueryResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryListQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryListQueryResult> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesAsync(cancellationToken);

                return new GetCategoryListQueryResult() { Categories = categories, TotalCategoriesCount = categories.Count };
            }
            catch(Exception ex)
            {
                throw new Exception("There is a problem in GetCategoryListQueryHandler", ex);
            }
        }
    }
}