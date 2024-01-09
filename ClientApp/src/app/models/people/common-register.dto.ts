import {CreateAddressDto} from "../create-address.dto";

export interface CommonRegisterDto {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  address: CreateAddressDto;
  pictureUrl: string;
  type: string;
}
