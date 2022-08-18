using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Mediator.Queries.Categories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Categories
{
    public class GetCategoryForEditingQueryHandler : IRequestHandler<GetCategoryForEditingQuery, GetCategoryForEditingQueryResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryForEditingQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryForEditingQueryResult> Handle(GetCategoryForEditingQuery request, CancellationToken cancellationToken)
        {
            var category = new Category();
            if (request.CategoryId != 0)
            {
                category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            }
            var listCategory = await _categoryRepository.OrderAllCategoryAsync(0, cancellationToken);

            return new GetCategoryForEditingQueryResult(category, listCategory);
        }
    }
}