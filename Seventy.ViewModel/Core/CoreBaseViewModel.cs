using AutoMapper;
using Seventy.DomainClass.Core;
using Seventy.ViewModels.CustomMapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel
{
    public abstract class CoreBaseViewModel
    {
        public int? ID { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ایجاد کننده")]
        public int? RegUserID { get; set; }

        [Display(Name = "زمان ایجاد")]
        public DateTime RegDate { get; set; } = DateTime.Now;
    }
    public interface ICoreBaseViewModelMap
    {
        public string Description { get; set; }
        public bool IsActive { get; set; } 
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; } 
    }
    public abstract class CoreBaseViewModelMap<TViewModel, TEntity, TKey> : IHaveCustomMapping, ICoreBaseViewModelMap
        where TViewModel : ICoreBaseViewModelMap
        where TEntity : CoreBase<TKey>, new()
        where TKey : struct
    {
        public TKey ID { get; set; }
      
        [Display(Name ="شرح")]
        public string Description { get; set; }
        
        [Display(Name ="فعال")]
        public bool IsActive { get; set; } = true;
       
        [Display(Name = "ایجاد کننده")]
        public int? RegUserID { get; set; }
       
        [Display(Name = "زمان ایجاد")]
        public DateTime RegDate { get; set; } = DateTime.Now;

        public virtual TEntity ToEntity()
        {
            var model = Mapper.Map<TEntity>(CastToDerivedClass(this));
            model.RegDate = model.RegDate == DateTime.MinValue ? DateTime.Now : model.RegDate;
            model.IsActive = true;
            return model;
        }

        /// <summary>
        /// Convert ViewModel to Entity for update operation
        /// </summary>
        /// <param name="entity">The reflected entity from database that need to update</param>
        /// <returns></returns>
        public virtual TEntity ToEntity(TEntity entity) // For Update
        {
            return Mapper.Map(CastToDerivedClass(this), entity);
        }

        /// <summary>
        /// Convert Entity to ViewModel for read operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TViewModel FromEntity(TEntity model)
        {
            return Mapper.Map<TViewModel>(model);
        }

        public virtual TViewModel FromEntityCustom(TEntity model)
        {
            var viewModel = Mapper.Map<TViewModel>(model);
            return viewModel;
        }

        protected TViewModel CastToDerivedClass(CoreBaseViewModelMap<TViewModel, TEntity, TKey> baseInstance)
        {
            return Mapper.Map<TViewModel>(baseInstance);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TViewModel, TEntity>();

            var dtoType = typeof(TViewModel);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TViewModel> mapping)
        {
        }
    }
    public abstract class CoreBaseViewModel<TViewModel, TEntity> : CoreBaseViewModelMap<TViewModel, TEntity, int>
        where TViewModel : ICoreBaseViewModelMap
        where TEntity : CoreBase<int>, new()
    {
    }
}
