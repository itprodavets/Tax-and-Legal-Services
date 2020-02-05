import {ApiCrudService} from "@/core/services/api.crud.service";
import {AxiosResponse} from "axios";
import {ReportData, ReportDataParseRequest, ReportDataValidationRequest, ValidationMessage} from "../models";

class CbcService extends ApiCrudService<number, any, any> {
	constructor() {
		super("reports", {
			baseURL: `${process.env.VUE_APP_CBC_REPORT_API}/v1`
		});
	}
	
	public parse(request: ReportDataParseRequest): Promise<AxiosResponse<ReportData>> {
		const data = new FormData();
		data.append("file", request.file);
		
		return this.instance.post<ReportData>(`reports/parse`, data, {
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		});
	}
	
	public generation(request: any): Promise<AxiosResponse> {
		return this.instance.get(`reports/parse`, {
			params: request,
			responseType: "blob"
		});
	}
	
	public validate(request: ReportDataValidationRequest): Promise<AxiosResponse<ValidationMessage>> {
		const params = new URLSearchParams();
		params.append("schema", request.schema);
		
		const data = new FormData();
		data.append("file", request.file);
		
		return this.instance.post<ValidationMessage>(`reports/validate`, data, {
			params: params,
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		});
	}
}

export default new CbcService();
