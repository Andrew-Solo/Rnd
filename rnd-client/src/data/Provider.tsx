import Result from "./Result";
import axios, {AxiosError, AxiosResponse} from "axios";
import Message from "./Message";

export default class Provider<T> {
  constructor(data: {host: string, url: string}) {
    this.host = data.host;
    this.url = data.url;
  }

  async get(identifier: string, params: {[prop:string]: any} = {}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.get(this.getUrl(identifier), {params})
    );
  }

  async list(identifier: string = "", params: {[prop:string]: any} = {}): Promise<Result<T[]>> {
    return this.processResponse(
      () => axios.get(this.getUrl(identifier), {params})
    );
  }

  async create(data: {[prop:string]: any}, identifier: string = ""): Promise<Result<T>> {
    return this.processResponse(
      () => axios.post(this.getUrl(identifier), data)
    );
  }

  async update(data: {[prop:string]: any}, identifier: string): Promise<Result<T>> {
    return this.processResponse(
      () => axios.put(this.getUrl(identifier), data)
    );
  }

  async delete(identifier: string, params: {[prop:string]: any} = {}): Promise<Result<T>> {
    return this.processResponse(
      () => axios.delete(this.getUrl(identifier), {params})
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

  getUrl(identifier: string | null = null): string {
    return !!identifier ? `${this.host}/${this.url}/${identifier}` : `${this.host}/${this.url}`;
  }

  host: string
  url: string
}