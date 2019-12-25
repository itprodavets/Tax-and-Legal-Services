export interface IApiCrudService<TKey, TRequestModel, TViewModel> {
    list(request?: TRequestModel): Promise<TViewModel[]>;
    get(id: TKey, model?: TRequestModel): Promise<TViewModel>;
    create<TCommand>(command: TCommand): Promise<TKey>;
    update<TCommand>(id: TKey, command: TCommand): Promise<void>;
    delete(id: TKey): Promise<void>;
  }