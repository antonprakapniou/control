namespace Control.BLL.Interfaces;

public interface IOwnerService : IGenericService<OwnerVM, Owner>
{
    #region Methods

    public Task<OwnerCreatingVM> SetOwnerSelectList(OwnerCreatingVM viewModel);

    #endregion
}
