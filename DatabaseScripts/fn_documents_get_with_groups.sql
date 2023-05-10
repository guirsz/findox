\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_documents_get_with_groups(
	in_document_id uuid)
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
		WHERE document_id = in_document_id
		AND deleted = FALSE;
END
$BODY$;

ALTER FUNCTION public.fn_documents_get_with_groups(uuid)
    OWNER TO postgres;