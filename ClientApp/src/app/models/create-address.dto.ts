export interface CreateAddressDto {
  street: string;
  city: string;
  county: string;
  country: string;
  zipCode: string;
  streetNumber: string;
  apartmentNumber?: string;
}
