import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserModel} from '../shared/models/user.model';
import {ResponseModel} from '../shared/models/response.model';
import {LogInResponceModel} from '../shared/models/logIn-responce.model';
import {environment} from '../../environments/environment';
import {ResetPasswordModel} from '../shared/models/reset-password.model';
import {SinginModel} from '../shared/models/singin.model';
import * as jwt_decode from 'jwt-decode';

@Injectable()
export class AuthService {

  private api = 'api';
  private control = 'Account';
  private actionSingIn = 'SingIn';
  private actionRegister = 'Register';
  private actionForgotPass = 'ForgotPassword';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}`;

  private token = '';
  public isAuth: boolean;
  public isAdmin: boolean;
  public user: string;

  constructor(private  http: HttpClient) {
    const token: string = localStorage.getItem('accessToken');
    if (token) {
      this.isAuth = true;
      this.user = localStorage.getItem('User');
      const jwtDecode = jwt_decode(token);
      this.isAdmin = jwtDecode['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'ADMIN';
    }
  }

  public async SingUp(user: UserModel): Promise<ResponseModel> {
    const urlPath = `${this.serverUrl}/${this.actionRegister}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, user).toPromise();
    return result;
  }

  public async SingIn(user: SinginModel): Promise<LogInResponceModel> {
    const urlPath = `${this.serverUrl}/${this.actionSingIn}`;
    const response: LogInResponceModel = await this.http.post<LogInResponceModel>(urlPath, user).toPromise();

    if (response.isValid) {
      this.setToken(response);
    }
    return response;
  }

  public logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('Role');
    localStorage.removeItem('User');
  }

  public async forgotPassword(user: ResetPasswordModel): Promise<ResponseModel> {
    const urlPath = `${this.serverUrl}/${this.actionForgotPass}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, user).toPromise();
    return result;
  }

  private setToken(response: LogInResponceModel) {
    localStorage.setItem('accessToken', response.accessToken);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('User', response.user);
  }
}
