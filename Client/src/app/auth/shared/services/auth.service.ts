import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserModel} from '../../../models/UserModel';
import {Responsemodel} from '../../../models/Responsemodel';
import {LogInResponceModel} from '../../../models/LogInResponceModel';
import {environment} from '../../../../environments/environment';

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

  public async SingUp(user: UserModel): Promise<Responsemodel> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionRegister;
    const result: Responsemodel = await this.http.post<Responsemodel>(urlPath, user).toPromise();
    return result;
  }

  public async SingIn(user: UserModel): Promise<LogInResponceModel> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionSingIn;
    const response: LogInResponceModel = await this.http.post<LogInResponceModel>(urlPath, user).toPromise();

    if (response.isValid) {
      this.setToken(response);
    }
    return response;
  }

  logout() {
    localStorage.clear();
  }

  public async forgotPassword(user: UserModel): Promise<Responsemodel> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionForgotPass;
    const result: Responsemodel = await this.http.post<Responsemodel>(urlPath, user).toPromise();
    return result;
  }

  private setToken(response: LogInResponceModel | null) {
    localStorage.setItem('accessToken', response.accessToken);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('Role', response.role);
    localStorage.setItem('User', response.user);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }
}
