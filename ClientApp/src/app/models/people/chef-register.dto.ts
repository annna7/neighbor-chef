import {CommonRegisterDto} from "./common-register.dto";

export interface ChefRegisterDto extends CommonRegisterDto {
  maxOrdersPerDay: number;
  description: string;
  advanceNoticeDays: number;
}
