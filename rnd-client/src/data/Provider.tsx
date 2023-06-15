import Result from "./Result";
import {Entity} from "../models/Unit";
import axios, {AxiosError} from "axios";

export default class Provider<T extends Entity> {
  constructor(data: {url: string}) {
    this.url = data.url;
  }

    async get(query: {[prop:string]: any} = {}): Promise<Result<T>> {
    const {id, ...params} = query;
    const url = !!id ? `${this.url}/${id}` : this.url;

    try {
      const response = await axios.get(url, {params});
      return JSON.parse(response.data);
    }
    catch (error: AxiosError | any) {
      console.log(error);
      if (!!error.response)  {
        return JSON.parse(error.response.data);
      } else {
        throw error;
      }
    }
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