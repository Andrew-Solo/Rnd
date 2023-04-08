import React, {useRef} from 'react';
import {
  Dropdown,
  Popover,
  Whisper,
  WhisperInstance,
  Stack,
  Avatar
} from 'rsuite';
import {NavLink} from "react-router-dom";

const renderAccountSpeaker = ({onClose, left, top, className}: any, ref) => {
  const handleSelect = () => onClose();

  return (
    <Popover ref={ref} className={className} style={{left, top, padding: "20px", minWidth: "200px"}} full>
      <Dropdown.Menu onSelect={handleSelect}>
        <Dropdown.Item as={NavLink} to={"/account"}>Аккаунт</Dropdown.Item>
        <Dropdown.Item>Выйти</Dropdown.Item>
      </Dropdown.Menu>
    </Popover>
  );
};

const Header = () => {
  const trigger = useRef<WhisperInstance>(null);

  return (
    <Stack className="header" spacing={8}>
      <Whisper placement="bottomEnd" trigger="click" ref={trigger} speaker={renderAccountSpeaker}>
        <Stack spacing={5}>
          <div style={{paddingBottom: 5}}>
            <strong>Username</strong>
          </div>
          <Avatar
            size="sm"
            circle
            src="https://avatars.githubusercontent.com/u/1203827"
            alt="Username"
            style={{marginLeft: 8}}
          />
        </Stack>
      </Whisper>
    </Stack>
  );
};

export default Header;
