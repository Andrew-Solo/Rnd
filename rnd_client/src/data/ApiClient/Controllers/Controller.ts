import $ from "jquery";

abstract class Controller {
  constructor(path: string) {
    this._path = path;
  }

  public abstract readonly name: string;

  public get url(): string {
    return this._path + "/" + this.name;
  }

  public getRequest(data: object, url: string, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Get, data, url, onSuccess, onError);
  }

  public createRequest(data: object, url: string, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Post, data, url, onSuccess, onError);
  }

  public updateRequest(data: object, url: string, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Put, data, url, onSuccess, onError);
  }

  public deleteRequest(data: object, url: string, onSuccess: any, onError: any): void {
    this.sendRequest(Method.Delete, data, url, onSuccess, onError);
  }

  private sendRequest(method: Method, data: object, url: string, onSuccess: (args: any) => any, onError: (args: any) => any): void {
    $.ajax(this.combineUrl(url), {method, data}).done(onSuccess).fail(onError);
  }

  private readonly _path: string;

  private combineUrl(url: string) {
    if (url) return this.url + "/" + url;
    return this.url;
  }
}

export enum Method {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export type SuccessCallback<T> = (response: DataResponse<T>) => void;
export type ErrorCallback = (response: Response) => void;

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