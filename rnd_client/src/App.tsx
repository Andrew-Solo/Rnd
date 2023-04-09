import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { IntlProvider } from 'react-intl';
import { CustomProvider } from 'rsuite';
import ruRU from 'rsuite/locales/ru_RU';
import locales from './locales';
import Root from './root';
import Dashboard from './pages/dashboard';
import Account from './pages/account';
import Error404Page from './pages/error/404';
import Error403Page from './pages/error/403';
import Error500Page from './pages/error/500';
import Error503Page from './pages/error/503';
import MembersPage from './pages/tables/members';
import VirtualizedTablePage from './pages/tables/virtualized';
import FormBasicPage from './pages/forms/basic';
import FormWizardPage from './pages/forms/wizard';
import CalendarPage from './pages/calendar';
import { appNavs } from './config';
import Registration from "@/pages/account/registration";
import Login from "@/pages/account/login/index";

const App = () => {
  return (
    <IntlProvider locale="ru" messages={locales.ru}>
      <CustomProvider locale={ruRU}>
        <Routes>
          <Route path="account">
            <Route index element={<Account />} />
            <Route path="login" element={<Login />} />
            <Route path="registration" element={<Registration />} />
          </Route>
          <Route path="/" element={<Root navs={appNavs} />}>
            <Route index element={<Dashboard />} />
            <Route path="dashboard" element={<Dashboard />} />
            <Route path="table-members" element={<MembersPage />} />
            <Route path="table-virtualized" element={<VirtualizedTablePage />} />
            <Route path="error-404" element={<Error404Page />} />
            <Route path="error-403" element={<Error403Page />} />
            <Route path="error-500" element={<Error500Page />} />
            <Route path="error-503" element={<Error503Page />} />
            <Route path="form-basic" element={<FormBasicPage />} />
            <Route path="form-wizard" element={<FormWizardPage />} />
            <Route path="calendar" element={<CalendarPage />} />
          </Route>
          <Route path="*" element={<Error404Page />} />
        </Routes>
      </CustomProvider>
    </IntlProvider>
  );
};

export default App;
