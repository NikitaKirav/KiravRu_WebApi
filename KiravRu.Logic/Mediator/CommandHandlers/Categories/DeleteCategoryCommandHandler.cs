using KiravRu.Logic.Interface.Categories;
using KiravRu.Logic.Mediator.Commands.Categories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
                _categoryRepository.Delete(category);
                await _categoryRepository.SaveChanges(cancellationToken);

                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception("There is a problem in DeleteCategoryCommandHandler", ex);
            }
        }
    }
}