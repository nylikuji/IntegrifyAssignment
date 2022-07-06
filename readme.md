Create a PostgreSQL server.

Create a database called "todolists"

Add sequences "users_id_seq" and "todolistings_id_seq" in the public schema's sequences

In the PostgreSQL server add tables using following commands:


#### Table for Users
-----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.users
(
    id bigint NOT NULL DEFAULT nextval('users_id_seq'::regclass),
    email text COLLATE pg_catalog."default",
    password text COLLATE pg_catalog."default",
    "JWTtoken" text COLLATE pg_catalog."default",
    datecreated timestamp with time zone,
    dateupdated timestamp with time zone,
    CONSTRAINT users_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.users
    OWNER to postgres;

#### Table for the Todo listings
-----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.todos
(
    name text COLLATE pg_catalog."default",
    description text COLLATE pg_catalog."default",
    datecreated timestamp with time zone,
    dateupdated timestamp with time zone,
    status text COLLATE pg_catalog."default",
    id bigint NOT NULL DEFAULT nextval('todolistings_id_seq'::regclass),
    userid bigint,
    CONSTRAINT todos_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.todos
    OWNER to postgres;

-----------------------------------------------------------------------------




UserContext.cs,
TodoContext.cs.
These code files include the connection strings to the server, they are configured to connect to tables within the public schema

#### Code files

Program.cs includes the main function. It initializes everything.

wwwroot includes files for the front-end app.

#### Controllers 

Controllers respond to all HTTP requests (GET, POST, DELETE, PUT). And execute their designated functions.
A controller function may have arguments, which are passed by the HTTP request parameters. In most cases it includes a JWT token parameter.

The JWT token parameter is used check who the request is coming from, using the GetIdByToken function.
This way we can validate if the user is accessing todo-listings that belongs to them.

#### Interfaces and Repositories

Interfaces in C# remind me of C++ header files, they define functions which can be called from a Repository.

A Repository has a Context object attached to it, which includes an EFCore DatabaseContext.

Repositories include code which executes database queries and actions. Like deleting, adding or updating an entry in the database.

#### Models and Contexts

The Context class files include code for initializing their DatabaseContext. A DatabaseContext is a gateway from the program to the database in question.

Models are simple class files where each property can be Get or Set.

The Contexts include an EFCore DBSet listing of these class objects. These lists are accessed only through their associated Repository
