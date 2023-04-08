import React from 'react';
import ErrorPage from '@/components/ErrorPage';
import { IconButton } from 'rsuite';
import ArrowLeftLine from '@rsuite/icons/ArrowLeftLine';

export default () => (
  <ErrorPage code={503}>
    <p className="error-page-title">Ой-ой… Вы нашли страницу с ошибкой</p>
    <p className="error-page-subtitle text-muted ">На данный момент сервис недоступен.</p>
    <IconButton icon={<ArrowLeftLine />} appearance="primary" href="/">
      На главную
    </IconButton>
  </ErrorPage>
);
