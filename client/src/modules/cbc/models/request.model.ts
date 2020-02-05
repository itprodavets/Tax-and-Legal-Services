import {TRequest} from '@/core/models';
import {ConstituentEntity, ReportData, ReportingEntity} from "@/modules/cbc/models/dto.model.js";


export interface ReportDataAddRequest {
	data: ReportData;
}

export interface ReportDataCreateRequest {
	data: ReportData;
}

export interface ReportDataUpdateRequest {
	data: ReportData;
}

export interface ReportDataParseRequest {
	schema:  string;
	file: File;
}

export interface ReportDataValidationRequest {
	schema:  string;
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

export interface ConstituentEntityUpdateRequest extends TRequest {
	reportId: string | number;
	constituentEntity: ConstituentEntity;
}

export interface ReportingEntityRequest {
	reportId: string | number;
}

export interface ReportingEntityAddRequest {
	reportId: string | number;
	reportingEntity: ReportingEntity;
}

export interface AdditionalInfoRequest extends TRequest {
	reportId: string | number;
}

export interface ReportBodyRequest extends TRequest{
	reportId: string | number;
}

