import { Lob } from "./lob.model";

export interface Plan {
  planId: number;
  planName: string;
  lobs: Lob[];
}
