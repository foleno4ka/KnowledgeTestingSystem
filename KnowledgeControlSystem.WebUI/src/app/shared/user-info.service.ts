import { Injectable } from '@angular/core';
import { TokenDto } from '../dto/token-dto.model';

@Injectable()
export class UserInfoService {

    private static USER_TOKEN_KEY: string = "userToken";
    private static USER_ROLES_KEY: string = "userRoles";

    setInfo(token: TokenDto) {
        localStorage.setItem(UserInfoService.USER_TOKEN_KEY, token.access_token);
        localStorage.setItem(UserInfoService.USER_ROLES_KEY, token.role)
    }

    isInRole(allowedRoles): boolean {
        var userRoles: string[] = JSON.parse(localStorage.getItem(UserInfoService.USER_ROLES_KEY));
        if (userRoles == null)
            return false;
        var isMatch = false;
        allowedRoles.forEach(role => {
            if (userRoles.indexOf(role) > -1) {
                isMatch = true;
                return false;
            }
        })
        return isMatch;
    }
}
