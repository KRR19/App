import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserModel} from '../shared/models/user.model';
import {ResponseModel} from '../shared/models/response.model';
import {LogInResponceModel} from '../shared/models/logIn-responce.model';
import {environment} from '../../environments/environment';
import {ResetPasswordModel} from '../shared/models/reset-password.model';
import {SinginModel} from '../shared/models/singin.model';

@Injectable()
export class AuthService {

  private Api = 'api';
  private control = 'Account';
  private ActionSingIn = 'SingIn';
  private ActionRegister = 'Register';
  private ActionForgotPass = 'ForgotPassword';
  private token = '';

  constructor(private  http: HttpClient) {
  }

  public async SingUp(user: UserModel): Promise<ResponseModel> {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionRegister}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, user).toPromise();
    return result;
  }

  public async SingIn(user: SinginModel): Promise<LogInResponceModel> {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionSingIn}`;
    const response: LogInResponceModel = await this.http.post<LogInResponceModel>(urlPath, user).toPromise();

    if (response.isValid) {
      this.setToken(response);
    }
    return response;
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('Role');
    localStorage.removeItem('User');
  }

  public async forgotPassword(user: ResetPasswordModel): Promise<ResponseModel> {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionForgotPass}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, user).toPromise();
    return result;
  }

  private setToken(response: LogInResponceModel | null) {
    localStorage.setItem('accessToken', response.accessToken);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('User', response.user);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }
}
