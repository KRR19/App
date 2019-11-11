import {Responsemodel} from './Responsemodel';

export interface LogInResponceModel extends Responsemodel {
  accessToken: string;
  refreshToken: string;
  role: string;
}
