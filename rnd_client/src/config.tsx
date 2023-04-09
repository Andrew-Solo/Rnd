import React from 'react';
import { Icon } from '@rsuite/icons';
import { VscTable, VscCalendar } from 'react-icons/vsc';
import { MdDashboard, MdModeEditOutline } from 'react-icons/md';

export const appNavs = [
  {
    eventKey: 'dashboard',
    icon: <Icon as={MdDashboard} />,
    title: 'Главная',
    to: '/dashboard'
  },
  {
    eventKey: 'calendar',
    icon: <Icon as={VscCalendar} />,
    title: 'Calendar',
    to: '/calendar'
  },
  {
    eventKey: 'tables',
    icon: <Icon as={VscTable} />,
    title: 'Tables',
    to: '/table-members',
    children: [
      {
        eventKey: 'members',
        title: 'Members',
        to: '/table-members'
      },
      {
        eventKey: 'virtualized',
        title: 'Virtualized Table',
        to: '/table-virtualized'
      }
    ]
  },
  {
    eventKey: 'forms',
    icon: <Icon as={MdModeEditOutline} />,
    title: 'Forms',
    to: '/form-basic',
    children: [
      {
        eventKey: 'form-basic',
        title: 'Basic',
        to: '/form-basic'
      },
      {
        eventKey: 'form-wizard',
        title: 'Wizard',
        to: '/form-wizard'
      }
    ]
  },
];
