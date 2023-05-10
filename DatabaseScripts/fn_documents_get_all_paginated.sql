\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_documents_get_all_paginated(
	in_limit integer,
	in_offset integer,
	in_filter_text character varying,
	in_user_id integer)
	RETURNS TABLE (
		document_id 	uuid,
		file_name 		character varying,
		granted_users 	integer[],
		granted_groups 	integer[]
	) 
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN	
	RETURN QUERY 
		SELECT 
			document_id,
			file_name,
			array(	select 
						grant_access_users.user_id 
					from grant_access_users 
					inner join users on grant_access_users.user_id = users.user_id and users.deleted = false
					where 
						documents.document_id = grant_access_users.document_id) as granted_users,
			array(	select 
						grant_access_groups.group_id 
					from grant_access_groups 
					inner join groups on groups.group_id = grant_access_groups.group_id and groups.deleted = false
					where documents.document_id = grant_access_groups.document_id) as granted_groups
		FROM documents
		WHERE file_name ILIKE in_filter_text 
		AND deleted = FALSE
		AND (in_user_id = 0 OR
			 EXISTS(select 1 
					from grant_access_users 
					where grant_access_users.document_id = documents.document_id 
					and grant_access_users.user_id = in_user_id 
					limit 1) OR
			 EXISTS(select 1 
					from grant_access_groups 
					inner join user_groups on grant_access_groups.group_id = user_groups.group_id
					where grant_access_groups.document_id = documents.document_id 
					and user_groups.user_id = in_user_id)
		)
		ORDER BY file_name
		LIMIT in_limit OFFSET in_offset;
END
$BODY$;

ALTER FUNCTION public.fn_documents_get_all_paginated(integer, integer, character varying, integer)
    OWNER TO postgres;
