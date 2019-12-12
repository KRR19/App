import {ResponseModel} from './response.model';

export interface LogInResponceModel extends ResponseModel {
  user?: string;
  role?: string;
  accessToken?: string;
  refreshToken?: string;
}
