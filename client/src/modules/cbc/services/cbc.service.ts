import {ApiCrudService} from "@/core/services/api.crud.service";
import api from "@/core/services/api";
import {AxiosRequestConfig, AxiosResponse} from "axios";
import {ReportData, ReportDataImportRequest} from "../models";

class CbcService extends ApiCrudService<number, any, any> {
	constructor() {
		super("cbc");
		this.requestConfig = {
			baseURL: `${process.env.VUE_APP_CBC_REPORT_API}/v1`
		} as AxiosRequestConfig;
	}
	
	public xmlToModel(request: ReportDataImportRequest): Promise<AxiosResponse<ReportData[]>> {
		return api.get<ReportData[]>(`xmlToModel`, {
			params: request
		});
	}
	
	public modelToXml(request: any): Promise<AxiosResponse<void>> {
		return api.get<void>(`modelToXml`, {
			params: request
		});
	}
	
	public xmlValidate(request: any): Promise<AxiosResponse<any>> {
		return api.get<any>(`xmlValidate`, {
			params: request
		});
	}
}

export default new CbcService();
