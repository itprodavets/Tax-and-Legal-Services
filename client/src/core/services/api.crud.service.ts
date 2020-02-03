import api from "./api";
import {AxiosRequestConfig, AxiosResponse} from "axios";

export interface IApiCrudService<TKey, TRequestModel, TViewModel> {
  list(request?: TRequestModel): Promise<AxiosResponse<TViewModel[]>>;
  
  get(id: TKey, model?: TRequestModel): Promise<AxiosResponse<TViewModel>>;
  
  create<TCommand>(command: TCommand): Promise<AxiosResponse<TKey>>;
  
  update<TCommand>(id: TKey, command: TCommand): Promise<AxiosResponse<void>>;
  
  delete(id: TKey): Promise<AxiosResponse<void>>;
}

export abstract class ApiCrudService<TKey, TRequestModel, TViewModel>
    implements IApiCrudService<TKey, TRequestModel, TViewModel> {
  private readonly _resource: string;
  
  protected constructor(resource: string) {
    this._resource = resource;
  }
  
  get requestConfig(): AxiosRequestConfig {
    return api.defaults;
  }
  
  set requestConfig(config: AxiosRequestConfig) {
    api.defaults = Object.assign(api.defaults, config);
  }
  
  public list(request?: TRequestModel): Promise<AxiosResponse<TViewModel[]>> {
    return api.get<TViewModel[]>(`${this._resource}/list`, {
      params: request
    });
  }
  
  public get(
      id: TKey,
      request?: TRequestModel
  ): Promise<AxiosResponse<TViewModel>> {
    return api.get<TViewModel>(`${this._resource}/${id}`, {
      params: request
    });
  }
  
  public create<TCommand>(command: TCommand): Promise<AxiosResponse<TKey>> {
    return api.post<TKey>(`${this._resource}`, command);
  }
  
  public update<TCommand>(
      id: TKey,
      command: TCommand
  ): Promise<AxiosResponse<void>> {
    return api.put(`${this._resource}/${id}`, command);
  }
  
  public delete(id: TKey): Promise<AxiosResponse<void>> {
    return api.delete(`${this._resource}/${id}`);
  }
}
