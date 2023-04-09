import React, {useState} from 'react';

import { Form, Button, Panel, InputGroup, Stack, Divider } from 'rsuite';
import EyeIcon from '@rsuite/icons/legacy/Eye';
import EyeSlashIcon from '@rsuite/icons/legacy/EyeSlash';
import { Link } from 'react-router-dom';
import Brand from '@/root/Brand';

const Registration = () => {
  const [visible, setVisible] = useState(false);
  const [form, setForm] = useState({});

  return (
    <Stack
      justifyContent="center"
      alignItems="center"
      direction="column"
      style={{
        height: '100vh'
      }}
    >
      <Brand style={{ marginBottom: 10 }} />
      <Panel
        header={<h3>Создать Аккаунт</h3>}
        bordered
        style={{ background: '#fff', width: 400 }}
      >
        <p>
          <span>Уже есть аккаунт?</span> <Link to="/account/login">Войти</Link>
        </p>

        <Divider/>

        <Form formValue={form} onChange={setForm} fluid>
          <Form.Group>
            <Form.ControlLabel>Логин</Form.ControlLabel>
            <Form.Control name="login" />
          </Form.Group>
          <Form.Group>
            <Form.ControlLabel>Email</Form.ControlLabel>
            <Form.Control name="email" />
          </Form.Group>
          <Form.Group>
            <Form.ControlLabel>Пароль</Form.ControlLabel>
            <InputGroup inside style={{ width: '100%' }}>
              <Form.Control
                name="password"
                type={visible ? 'text' : 'password'}
                autoComplete="off"
              />
              <InputGroup.Button onClick={() => setVisible(!visible)}>
                {visible ? <EyeIcon /> : <EyeSlashIcon />}
              </InputGroup.Button>
            </InputGroup>
          </Form.Group>
          <Form.Group>
            <Form.ControlLabel>Подтверждение пароля</Form.ControlLabel>
            <Form.Control name="confirm-password" type="password" />
          </Form.Group>
          <Form.Group>
            <Stack spacing={6}>
              <Button onClick={() => console.log(form)} appearance="primary" type="submit">Создать</Button>
            </Stack>
          </Form.Group>
        </Form>
      </Panel>
    </Stack>
  );
};

export default Registration;
