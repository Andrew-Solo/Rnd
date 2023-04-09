import React from 'react';
import {Form, Button, Panel, Stack, Divider} from 'rsuite';
import { Link } from 'react-router-dom';
import Brand from '@/root/Brand';

const Login = () => {
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

      <Panel bordered style={{ background: '#fff', width: 400 }} header={<h3>Войти</h3>}>
        <p style={{ marginBottom: 10 }}>
          <span className="text-muted">Нет аккаунта? </span>{' '}
          <Link to="/account/registration"> Зарегестрирорваться</Link>
        </p>

        <Divider/>

        <Form fluid>
          <Form.Group>
            <Form.ControlLabel>Логин или email</Form.ControlLabel>
            <Form.Control name="login" />
          </Form.Group>
          <Form.Group>
            <Form.ControlLabel>
              <span>Пароль</span>
              <a style={{ float: 'right' }}>Забыли пароль?</a>
            </Form.ControlLabel>
            <Form.Control name="password" type="password" />
          </Form.Group>
          <Form.Group>
            <Button appearance="primary">Войти</Button>
          </Form.Group>
        </Form>
      </Panel>
    </Stack>
  );
};

export default Login;
