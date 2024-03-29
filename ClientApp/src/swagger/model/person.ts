/**
 * Neighbor Chef API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 *
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */import { Address } from './address';
import { ApplicationUser } from './applicationUser';


export interface Person {
    id?: string;
    dateCreated?: Date;
    dateModified?: Date;
    firstName?: string;
    lastName?: string;
    applicationUserId?: string;
    applicationUser?: ApplicationUser;
    addressId?: string;
    address?: Address;
    profilePictureUrl?: string;
}
