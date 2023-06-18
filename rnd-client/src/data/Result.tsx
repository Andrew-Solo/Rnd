import Message from "./Message";

export default class Result<T> {
  constructor(success: boolean, message: Message | string, data: T) {
    this.success = success;
    this.data = data;
    this.message = typeof message === "string" ? new Message({title: message}) : message;
  }

  success: boolean
  data: T
  message: Message
}