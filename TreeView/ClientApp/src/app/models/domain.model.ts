import { Measure } from "./measure.model";

export interface Domain {
  domainId: number;
  domainName: string;
  measures: Measure[];
}
