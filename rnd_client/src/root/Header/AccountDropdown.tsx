import {Dropdown, Popover} from "rsuite";
import {NavLink} from "react-router-dom";
import React from "react";
import {UserRole} from "@/data/ApiClient/Controllers/UsersController";
import Cookie from "@/data/Cookie";

const AccountDropdown = ({onClose, left, top, className}: any, ref) => {
  const handleSelect = () => onClose();
  const user = Cookie.user;

  return (
    <Popover ref={ref} className={className} style={{left, top, padding: "20px", minWidth: "200px"}} full>
      <Dropdown.Menu onSelect={handleSelect}>
        {user.role == UserRole.Guest
          ? <>
            <Dropdown.Item as={NavLink} to={"/account/registration"}>Регистрация</Dropdown.Item>
            <Dropdown.Item as={NavLink} to={"/account/login"}>Вход</Dropdown.Item>
          </>
          : <>
            <Dropdown.Item as={NavLink} to={"/account"}>Аккаунт</Dropdown.Item>
            <Dropdown.Item>Выйти</Dropdown.Item>
          </>
        }
      </Dropdown.Menu>
    </Popover>
  );
};

export default AccountDropdown;