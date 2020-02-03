import {TRequest} from '@/core/models';
import {ConstituentEntity, SupportedSchema} from "@/modules/cbc/models/dto.model.js";


export interface ReportDataImportRequest {
	schema:  SupportedSchema;
	file: File;
}

export interface ReportRequest extends TRequest {
	reportDataId: string | number;
}

export interface ReportCreateRequest extends TRequest {
	reportDataId: string | number;
}


export interface ConstituentEntityRequest extends TRequest {
	reportId: string | number;
}
export interface ConstituentEntityCreateRequest extends TRequest {
	reportId: string | number;
	constituentEntity: ConstituentEntity;
}


export interface AdditionalInfoRequest extends TRequest {
	reportId: string | number;
}

export interface ReportBodyRequest extends TRequest{
	reportId: string | number;
}

