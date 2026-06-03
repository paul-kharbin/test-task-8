# Candidate Tracker

SPA-клиент React Router Framework Mode.

```sh
npm install
npm run dev
```

Приложение доступно по адресу `http://localhost:5173`. Запросы `/api`
проксируются в ASP.NET-приложение на `http://localhost:5264`.

```sh
npm run typecheck
npm run build
```

Для публикации React-приложения в ASP.NET-проект:

```sh
npm run publish:aspnet
```

Команда собирает SPA и копирует результат в `../TestTask.Web/wwwroot`.
