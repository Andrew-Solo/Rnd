import {Guest, User} from "@/data/ApiClient/Controllers/UsersController";

class Cookie {
  public static get user(): User {
    return this.getCookie("user") ?? new Guest();
  }

  public static set user(value) {
    this.setCookie("user", value);
  }

  private static setCookie(name: string, value: any, expiration = 7) {
    const json = JSON.stringify(value);
    const date = new Date();
    date.setTime(date.getTime() + (expiration * 24 * 60 * 60 * 1000));
    document.cookie = `${name}=${json}; expires=${date.toUTCString()}; path=/`;
  }

  private static getCookie(name: string): any {
    const cookie = `; ${document.cookie}`;
    const parts = cookie.split( `; ${name}=`);

    if (parts.length == 2) {
      return JSON.parse(parts.pop()?.split(";").shift() ?? "null");
    }
  }
}

export default Cookie;