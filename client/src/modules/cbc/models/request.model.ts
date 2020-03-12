import {AdditionalInfo, ConstituentEntity, Message, Report, ReportBody, ReportData, ReportingEntity} from "@/modules/cbc/models/dto.model.js";


export interface ReportDataAddRequest {
	data: ReportData;
}

export interface ReportDataRequest {
}

export interface ReportDataCreateRequest {
	data: ReportData;
}

export interface ReportDataCreateReportRequest {
	id: string | number;
	report: Report;
}

export interface ReportDataUpdateRequest {
	data: ReportData;
}

export interface ReportDataUpdateReportRequest {
	id: string | number;
	report: Report;
}

export interface ReportDataUpdateMessageRequest {
	reportDataId: string | number;
	message: Message;
}

export interface ReportDataParseRequest {
	file: File;
}

export interface ReportDataGenerateRequest {
	data: ReportData;
}

export interface ReportDataValidationRequest {
	schema: string;
	file: File;
}

export interface ReportRequest {
	reportDataId: string | number;
}

export interface ReportCreateRequest {
	reportDataId: string | number;
	report: Report;
}

export interface ReportUpdateRequest {
	reportDataId: string | number;
	report: Report;
}

export interface ConstituentEntityRequest {
	reportId: string | number;
}

export interface ConstituentEntityCreateRequest {
	reportId: string | number;
	constituentEntity: ConstituentEntity;
}

export interface ConstituentEntityUpdateRequest {
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

export interface AdditionalInfoRequest {
	reportId: string | number;
}

export interface AdditionalInfoCreateRequest {
	reportId: string | number;
	additionalInfo: AdditionalInfo;
}

export interface AdditionalInfoUpdateRequest {
	reportId: string | number;
	additionalInfo: AdditionalInfo;
}

export interface ReportBodyRequest {
	reportId: string | number;
}

export interface ReportBodyCreateRequest {
	reportId: string | number;
	reportBody: ReportBody;
}

export interface ReportBodyUpdateRequest {
	reportId: string | number;
	reportBody: ReportBody;
}

