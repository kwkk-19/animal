using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public float swingSpeed = 200.0f;   // �o�b�g�̐U�鑬��
    public float swingAngle = 90.0f;    // �o�b�g���U���p�x
    public float hitForce = 500.0f;     // �{�[���ɗ^�����
    public Vector3 initialPosition;    // �o�b�g�̏����ʒu
    public Quaternion initialRotation; // �o�b�g�̏�����]

    private bool isSwinging = false;    // �o�b�g���U���Ă��邩�ǂ����𔻒�
    private float currentAngle = 0.5f;  // ���݂̊p�x
    private bool swingDirection = true; // �U������itrue: �O���Afalse: �߂�j

    void Start()
    {
        // �����ʒu�Ɖ�]���L�^
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // �X�y�[�X�L�[���������Ƃ��Ƀo�b�g��U��
        if (Input.GetKeyDown(KeyCode.Space) && !isSwinging)
        {
            isSwinging = true;
            swingDirection = true; // �U��n�߂������ݒ�
        }

        // �o�b�g���U���Ă���ꍇ
        if (isSwinging)
        {
            float angleChange = swingSpeed * Time.deltaTime;

            if (swingDirection)
            {
                currentAngle += angleChange;
                if (currentAngle >= swingAngle)
                {
                    swingDirection = false; // �U��߂�
                }
            }
            else
            {
                currentAngle -= angleChange;
                if (currentAngle <= 0)
                {
                    currentAngle = 0;
                    isSwinging = false; // �U�蓮����I��

                    // �o�b�g�̈ʒu�Ɖ�]�����ɖ߂�
                    transform.position = initialPosition;
                    transform.rotation = initialRotation;
                }
            }

            // �o�b�g�̊p�x���X�V�iY����]�j
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    // �o�b�g�ƃ{�[�����Փ˂����Ƃ��ɌĂ΂��
    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���{�[�����ǂ����𔻒�
        if (collision.gameObject.CompareTag("Ball"))
        {
            // �{�[����Rigidbody���擾
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // �o�b�g�̑O�����Ƀ{�[���ɗ͂�������
                Vector3 hitDirection = transform.right; // �o�b�g�̉E�����ɔ�΂�
                ballRigidbody.AddForce(hitDirection * hitForce);
            }
        }
    }
}
