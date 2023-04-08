import $ from "jquery";

abstract class Controller {
  constructor(path: string) {
    this._path = path;
  }

  public abstract readonly name: string;

  public get url(): string {
    return this._path + "/" + this.name;
  }

  public getRequest(data: object, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Get, data, onSuccess, onError);
  }

  public createRequest(data: object, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Post, data, onSuccess, onError);
  }

  public updateRequest(data: object, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Put, data, onSuccess, onError);
  }

  public deleteRequest(data: object, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Delete, data, onSuccess, onError);
  }

  private sendRequest(method: Method, data: object, onSuccess: (args: any) => any, onError: (args: any) => any): void {
    $.ajax(this.url, {method, data}).done(onSuccess).fail(onError);
  }

  private readonly _path: string;
}

export enum Method {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export interface Response {
  message: Message;
}

export interface DataResponse<T> extends Response {
  data: T;
}

export interface Message {
  title: string;
  details: string[];
  tooltips: { [key: string]: string };
}

export default Controller;