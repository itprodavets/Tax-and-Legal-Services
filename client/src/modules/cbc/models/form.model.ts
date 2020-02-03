import {ReferenceBook} from "@/core/models";
import {AddressFix, AddressTypeEnum, Doc, DocTypeEnum} from "@/modules/cbc/models/dto.model";
import {Country} from "@/modules/country/models";

export interface AddressForm {
	id: string | number;
	jurisdiction: Country;
	type: ReferenceBook<AddressTypeEnum>;
	isFixed: boolean;
	fix: AddressFix;
	free: string;
}

export interface TinForm {
	id: string | number;
	jurisdiction: Country;
	tin: string;
}

export interface InForm {
	id: string | number;
	jurisdiction: Country;
	in: string;
	type: string;
}

export interface DocForm {
	type: ReferenceBook<DocTypeEnum>;
	refId: string;
	corrMessageRefId: string;
	corrDocRefId: string;
}