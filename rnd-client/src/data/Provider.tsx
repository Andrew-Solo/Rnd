import Result from "./Result";
import axios, {AxiosError, AxiosResponse} from "axios";
import Message from "./Message";

export default class Provider<T> {
  constructor(data: {host: string, url: string}) {
    this.host = data.host;
    this.url = data.url;
  }

  async get(userId: string, identifier: string, params: {[prop:string]: any} = {}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.get(this.getUrl(userId, identifier), {params})
    );
  }

  async list(userId: string, identifier: string = "", params: {[prop:string]: any} = {}): Promise<Result<T[]>> {
    return this.processResponse(
      () => axios.get(this.getUrl(userId, identifier), {params})
    );
  }

  async create(userId: string, identifier: string, data: {[prop:string]: any}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.post(this.getUrl(userId, identifier), data)
    );
  }

  async update(userId: string, identifier: string, data: {[prop:string]: any}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.put(this.getUrl(userId, identifier), data)
    );
  }

  async delete(userId: string, identifier: string, params: {[prop:string]: any} = {}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.delete(this.getUrl(userId, identifier), {params})
    );
  }

  async processResponse<TData>(sendRequest: () => Promise<AxiosResponse<any, any>>): Promise<Result<TData>> {
    try {
      const response = await sendRequest();
      return response.data;
    }
    catch (error: AxiosError | any) {
      if (!!error.response)  {
        return error.response.data;
      } else {
        return new Result<TData>(false, new Message({title: error.message, details: [error.stack]}), null!)
      }
    }
  }

  getUrl(userId: string, identifier: string | null = null): string {
    return !!identifier ? `${this.host}/${userId}/${this.url}/${identifier}` : `${this.host}/${userId}/${this.url}`;
  }

  host: string
  url: string
}