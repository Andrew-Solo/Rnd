import Result from "./Result";
import {Entity} from "../models/Unit";

export default class Provider<T extends Entity> {
  constructor(data: {url: string}) {
    this.url = data.url;
  }

  get(query: {[prop:string]: any} = {}): Result<T> {
    return {} as Result<T>;
  }

  list(query: {[prop:string]: any} = {}): Result<T[]> {
    return {} as Result<T[]>;
  }

  create(entity: T): Result<T> {
    return {} as Result<T>;
  }

  update(entity: T): Result<T> {
    return {} as Result<T>;
  }

  delete(id: string | null = null): Result<T> {
    return {} as Result<T>;
  }

  url: string
}