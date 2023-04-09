import React, {useRef} from 'react';
import {Whisper, WhisperInstance, Stack, Avatar} from 'rsuite';
import * as images from '@/images/mokups';
import AccountDropdown from "@/root/Header/AccountDropdown";
import Cookie from "@/data/Cookie";

const Header = () => {
  const trigger = useRef<WhisperInstance>(null);
  const user = Cookie.user;

  return (
    <Stack className="header" spacing={8}>
      <Whisper placement="bottomEnd" trigger="click" ref={trigger} speaker={AccountDropdown}>
        <Stack spacing={5}>
          <div style={{paddingBottom: 5}}>
            <strong>{user.login}</strong>
          </div>
          <Avatar
            size="sm"
            circle
            src={images.Avatar}
            alt="Username"
            style={{marginLeft: 8}}
          />
        </Stack>
      </Whisper>
    </Stack>
  );
};

export default Header;
