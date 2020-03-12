export enum IEnum {}
export type EnumUnion<T extends typeof IEnum> = T;
export type EnumUnionType<T extends typeof IEnum> = T[keyof T];
