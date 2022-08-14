using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Mediator.Commands.Categories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Categories
{
    public class SetCategoryCommandHandler : IRequestHandler<SetCategoryCommand, SetCategoryCommandResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public SetCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<SetCategoryCommandResult> Handle(SetCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category
                {
                    Id = request.Id,
                    Description = request.Description,
                    ImagePath = request.ImagePath,
                    ImageText = request.ImageText,
                    Name = request.Name,
                    NestingLevelId = request.NestingLevelId,
                    OrderItem = request.OrderItem,
                    Visible = request.Visible
                };
                _categoryRepository.Save(category);
                await _categoryRepository.SaveChanges(cancellationToken);


                var listCategory = await _categoryRepository.OrderAllCategoryAsync(0, cancellationToken);

                return new SetCategoryCommandResult(category, listCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("There is a problem in SetCategoryCommandHandler", ex);
            }
        }
    }
}