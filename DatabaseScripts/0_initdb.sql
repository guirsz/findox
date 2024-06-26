CREATE DATABASE findoxdb;

\c findoxdb;

CREATE TABLE IF NOT EXISTS users (
	user_id         serial PRIMARY KEY,
	user_name	    VARCHAR ( 100 ) NOT NULL,
	email      		VARCHAR ( 100 ) UNIQUE NOT NULL,
	password_hash   BYTEA NOT NULL,
	password_salt	BYTEA NOT NULL,
	role_id         INT NOT NULL,
	deleted			BOOLEAN DEFAULT FALSE,
	created_date	TIMESTAMP NOT NULL,
	created_by 		INT,
	updated_date	TIMESTAMP NOT NULL,
	updated_by		INT
);
CREATE TABLE IF NOT EXISTS groups (
	group_id    	serial PRIMARY KEY,
	group_name  	VARCHAR ( 100 ) UNIQUE NOT NULL,
	deleted			BOOLEAN DEFAULT FALSE,
	created_date	TIMESTAMP NOT NULL,
	created_by 		INT,
	updated_date	TIMESTAMP NOT NULL,
	updated_by		INT
);
CREATE TABLE IF NOT EXISTS user_groups (
	group_id    	INT NOT NULL,
	user_id			INT NOT NULL,
	grouped_date 	TIMESTAMP NOT NULL,
	grouped_by 		INT NOT NULL,
	PRIMARY KEY (group_id, user_id),
	CONSTRAINT  fk_user_groups_groups FOREIGN KEY (group_id) REFERENCES groups(group_id),
	CONSTRAINT  fk_user_groups_users FOREIGN KEY (user_id) REFERENCES users(user_id)
);
CREATE TABLE IF NOT EXISTS documents (
	document_id		UUID PRIMARY KEY,
	file_name  		VARCHAR ( 100 ) NOT NULL,
	file_length  	BIGINT NOT NULL,
	deleted			BOOLEAN DEFAULT FALSE,
	created_date	TIMESTAMP NOT NULL,
	created_by 		INT,
	updated_date	TIMESTAMP NOT NULL,
	updated_by		INT
);
CREATE TABLE IF NOT EXISTS grant_access_users (
	document_id		 	UUID NOT NULL,
	user_id				INT NOT NULL,
	granted_date 		TIMESTAMP NOT NULL,
	granted_by 			INT NOT NULL,
	PRIMARY KEY (document_id, user_id),
	CONSTRAINT  fk_grant_access_users_documents FOREIGN KEY (document_id) REFERENCES documents(document_id),
	CONSTRAINT  fk_grant_access_users_users FOREIGN KEY (user_id) REFERENCES users(user_id)
);
CREATE TABLE IF NOT EXISTS grant_access_groups (
	document_id		 	UUID NOT NULL,
	group_id			INT NOT NULL,
	granted_date 		TIMESTAMP NOT NULL,
	granted_by 			INT NOT NULL,
	PRIMARY KEY (document_id, group_id),
	CONSTRAINT  fk_grant_access_groups_documents FOREIGN KEY (document_id) REFERENCES documents(document_id),
	CONSTRAINT  fk_grant_access_groups_groups FOREIGN KEY (group_id) REFERENCES groups(group_id)
);
